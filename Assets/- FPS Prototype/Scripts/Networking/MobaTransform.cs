using UnityEngine;
using UnityEngine.Networking;

public class MobaTransform : NetworkBehaviour
{
    FPSRPGPrototype.BaseClasses.Player m_Owner;

    [HideInInspector]
    public bool isStanding = true;

    // [Tooltip("Any server-side movement that is below this number - will not be sent to clients.")]
    float movementTheshold = 0.001f;
    // [Tooltip("Any server-side rotation that is below this number (in degrees) - will not be sent to clients.")]
    float angleThreshold = 0.5f;

    [Tooltip("If the new position we received is too far (by this number) from our current position, we teleport there immediately instead of interpolating.")]
    public float teleportTreshold = 0.5f;

    Vector3 ov_PrevPosition;
    Quaternion ov_PrevRotation;
    Vector3 m_NewPosition;
    Quaternion m_NewRotation;
    public Vector3 m_CurrentDestination;
    float ov_LastClientSyncTime;
    float fraction;

    // serves to indicate what we're sending/receiving
    uint bitmask;

    [Range(2, 30), Tooltip("How many times per second will we send/receive the position. Recommended number: 10 for mobs, 25 for players. Don't change on runtime.")]
    public int UpdatesPerSecond = 25;

    // this variable affects the number of frames it'll take to lerp between initial position and received position
    int multiplier;

    void Awake()
    {
        m_Owner = gameObject.GetComponent<FPSRPGPrototype.BaseClasses.Player>();

        ov_PrevPosition = transform.position;
        ov_PrevRotation = transform.rotation;
        m_NewPosition = transform.position;
        m_NewRotation = transform.rotation;
        m_CurrentDestination = transform.position;

        fraction = 1;

        // This formula allows us to find the correct multiplier that is used in the Update function. Read more on that there first, then come back here and read this explanation if you like.
        // We take the number of updates per second and convert them into how often an update will happen in milliseconds. We add 0.5 for lag variation and round it up. Divide 60 by it.
        // So for example let's say we send our position 25 times per second:
        // 60 / 25 = 2.4
        // 2.4 + 0.5 = 2.9
        // Ceil(2.9) = 3
        // 60 / 3 = 20
        multiplier = Mathf.FloorToInt(60f / Mathf.Ceil((60f / UpdatesPerSecond) + 0.5f));
    }

    // happens before Update()
    void FixedUpdate()
    {
        if (isServer)
        {
            FixedUpdateServer();
        }
        if (isClient)
        {
            FixedUpdateClient();
        }
    }

    private void FixedUpdateServer()
    {
        if ((((base.syncVarDirtyBits == 0) && NetworkServer.active) && isServer) && (this.GetNetworkSendInterval() != 0f))
        {
            Vector3 vector = transform.position - ov_PrevPosition;
            if (vector.magnitude >= movementTheshold)
            {
                m_SetDirtyBit(1);
            }
            if (Quaternion.Angle(this.ov_PrevRotation, base.transform.rotation) >= angleThreshold)
            {
                m_SetDirtyBit(2);
            }
            if (hasDestinationChanged())
            {
                m_SetDirtyBit(4);
            }
        }
    }

    void FixedUpdateClient()
    {
        if (((((ov_LastClientSyncTime != 0f) && (NetworkServer.active || NetworkClient.active)) && (base.isServer || base.isClient)) && (this.GetNetworkSendInterval() != 0f)) && !base.hasAuthority)
        {
            // anything you want to do here?
            ;
        }
    }

    void Update()
    {
        if (!isServer)
        {
            // Let's say we want to get 25 updates per second, sometimes less or more depending on lag variation.
            // Due to that we want to reach the correct position in a little over 40ms. This way, we usually avoid a stop.
            // Lerp() gets a fraction value between 0 and 1. This is how far we went from A to B.
            // Our fraction variable would reach 1 in over 40ms if we multiply deltaTime by 20.
            // So if we send position 25 times per second, our multiplier is 20. That means the fraction will be 0.32, then 0.64, then 0.96, then 1. It will be executed in 4 frames (almost in 3 though).
            fraction = fraction + Time.deltaTime * (multiplier);
            // deltaTime = 1 / 60 = 0.016s
            // if the multiplier is 20, the fraction will be: 0.32... 0.62... 0.96... then 1. It will be executed in 4 frames (almost entirely in 3, though, but we'll use 1 more just in case of lag)

            // a few other examples of the multiplier's effect on the formula:
            // if the multiplier is 9, the fraction will be: 0.15... 0.30... 0.45... 0.60... 0.75... 0.90... 1.05... (1.05 is clamped to 1)... so if the value is 9, the movement is executed in 7 frames
            // if the multiplier is 8, the fraction will be: 0.13... 0.26... 0.40... 0.53... 0.66... 0.8... 0.93... 1... will be executed in 8 frames
            // if the multiplier is 7, the movement will be executed in 9 frames
            // the lower the multiplier, the more frames it'll take for the movement to be executed

            transform.position = Vector3.Lerp(ov_PrevPosition, m_NewPosition, fraction);
            transform.rotation = Quaternion.Lerp(ov_PrevRotation, m_NewRotation, fraction);

            // we need to check the distance between m_currentDestination and transform.position, but we only need to take into account the X-axis and the Z-axis.
            // the Y-axis is irrelevant, but if you absolutely want to compare it as well, give it more leeway, almost up to 0.1 units
            Vector2 curDest = new Vector2(m_CurrentDestination.x, m_CurrentDestination.z);
            Vector2 curPos = new Vector2(transform.position.x, transform.position.z);
            isStanding = (curDest - curPos).magnitude < 0.1f; // You can experiment a little with the 0.1 value. It's how far from the destination that you will start playing the Standing animation.
        }
    }

    // server-side serialization
    public override bool OnSerialize(NetworkWriter writer, bool initialState)
    {
        if (!initialState)
        {
            if (base.syncVarDirtyBits == 0)
            {
                writer.WritePackedUInt32(0);
                return false;
            }
            writer.WritePackedUInt32(bitmask);
        }
        SerializeModeTransform(writer, initialState);
        return true;
    }

    // client-side deserialization
    public override void OnDeserialize(NetworkReader reader, bool initialState)
    {
        // OnDeserialized is sometimes fired off even though we received nothing from the server. When that happens, the reader reads bitmask as null.
        if ((!isServer || !NetworkServer.localClientActive) && (initialState || ((bitmask = reader.ReadPackedUInt32()) != 0)))
        {
            UnserializeModeTransform(reader, initialState);
            ov_LastClientSyncTime = Time.time;
        }
    }

    private void SerializeModeTransform(NetworkWriter writer, bool initialState)
    {
        // writing position
        if (initialState || isBitDirty(1))
            writer.Write(transform.position);

        // writing rotation
        if (initialState || isBitDirty(2))
            NetworkTransform.SerializeRotation3D(writer, transform.rotation, NetworkTransform.AxisSyncMode.AxisY, NetworkTransform.CompressionSyncMode.Low);

        // Uncomment here to test: need some interaction with fps controller
        
        // writing destination
        //if (initialState || isBitDirty(4))
        //    writer.Write(m_Owner.Mover.currentDestination);

        ov_PrevPosition = transform.position;
        ov_PrevRotation = transform.rotation;
        //m_CurrentDestination = m_Owner.Mover.currentDestination;

    }

    private void UnserializeModeTransform(NetworkReader reader, bool initialState)
    {
        fraction = 0;
        ov_PrevPosition = transform.position;
        ov_PrevRotation = transform.rotation;

        if (initialState || isBitDirty(1))
            m_NewPosition = reader.ReadVector3();

        if (initialState || isBitDirty(2))
            m_NewRotation = NetworkTransform.UnserializeRotation3D(reader, NetworkTransform.AxisSyncMode.AxisY, NetworkTransform.CompressionSyncMode.Low);

        if (initialState || isBitDirty(4))
            m_CurrentDestination = reader.ReadVector3();

        // if the difference between oldPos and newPos is big enough to warrant an immediate teleportation
        if (Vector2.Distance(new Vector2(m_NewPosition.x, m_NewPosition.z), new Vector2(transform.position.x, transform.position.z)) >= teleportTreshold)
        {
            fraction = 1;
            ov_PrevPosition = m_NewPosition;
            ov_PrevRotation = m_NewRotation;
        }
    }

    public override float GetNetworkSendInterval()
    {
        return 1f / UpdatesPerSecond;
    }

    private bool hasDestinationChanged()
    {
        //if (m_CurrentDestination != m_Owner.Mover.currentDestination)
        //    return true;
        //else
            return false;
    }

    // Parameter 1 will set ...000001, 2 will set ...000010, 4 will set ...000100 and so on.
    private void m_SetDirtyBit(uint dirtyBit)
    {
        base.SetDirtyBit(1);
        bitmask |= dirtyBit;
    }

    // Parameter 1 will check ...000001, 2 will check ...000010, 4 will check ...000100 and so on.
    private bool isBitDirty(uint dirtyBit)
    {
        return (bitmask & dirtyBit) != 0;
    }
}
