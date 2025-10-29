using System.Collections.Generic;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions
{

	public class Patrol_ACT : ActionTask
	{
		public BBParameter<List<Transform>> patrolPoints;
		public float targetReachedDistance;
		public float sampleInterval;

		public BBParameter<Vector3> destination;

		private NavMeshAgent m_navAgent;

		private int m_currentTargetIndex = 0;
		private float m_timeSinceLastSample;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit()
		{
			if (!destination.useBlackboard)
			{
				Debug.LogError("Destination must be a blackboard variable");
			}

			m_navAgent = agent.GetComponent<NavMeshAgent>();

			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute()
		{
			destination.value = patrolPoints.value[m_currentTargetIndex].position;
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate()
		{
			m_timeSinceLastSample += Time.deltaTime;

			if (m_timeSinceLastSample < sampleInterval) { return; }

			destination.value = patrolPoints.value[m_currentTargetIndex].position;
			if (m_navAgent.hasPath && m_navAgent.remainingDistance <= targetReachedDistance)
			{
				m_currentTargetIndex = (m_currentTargetIndex + 1) % patrolPoints.value.Count;
				destination.value = patrolPoints.value[m_currentTargetIndex].position;
			}

			m_timeSinceLastSample -= sampleInterval;
		}

		//Called when the task is disabled.
		protected override void OnStop()
		{

		}

		//Called when the task is paused.
		protected override void OnPause()
		{

		}
	}
}