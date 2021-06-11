using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class AgentLearner : Agent
{
    private BufferSensorComponent _buffer;
    private BulletManager _bulletManager;

    public float agentSpeed = 6;

    // Update is called once per frame
    private void Awake()
    {
        _buffer = GetComponent<BufferSensorComponent>();
        _bulletManager = FindObjectOfType<BulletManager>();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);

        foreach(GameObject bullet in _bulletManager.bullets)
        {
            if (bullet.activeInHierarchy)
            {
                float[] overseer = { bullet.transform.position.x, bullet.transform.position.y, bullet.GetComponent<Rigidbody>().velocity.y };
                _buffer.AppendObservation(overseer);
            }
        }
    }
    public override void OnEpisodeBegin()
    {
        base.OnEpisodeBegin();
        SetReward(1f);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float X = actions.ContinuousActions[0];
        transform.position += new Vector3(X, 0) * agentSpeed * Time.deltaTime;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousAction = actionsOut.ContinuousActions;
        continuousAction[0] = Input.GetAxisRaw("Horizontal");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("die"))
        {
            collision.gameObject.SetActive(false);
            SetReward(-1f);
            EndEpisode();
            transform.localPosition = Vector3.zero;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        AddReward(-.1f);
    }
}
