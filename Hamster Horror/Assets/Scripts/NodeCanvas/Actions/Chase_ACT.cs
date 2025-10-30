using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions
{

	public class Chase_ACT : ActionTask
	{
		public BBParameter<Transform> target;
		public float sampleInterval;
		public float targetReachedDistance;

		public BBParameter<Vector3> destination;

		private float m_timeSinceLastSample;

		private NavMeshAgent m_navAgent;


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

		}

		//Called once per frame while the action is active.
		protected override void OnUpdate()
		{
			if (m_navAgent.hasPath && m_navAgent.remainingDistance < targetReachedDistance)
			{
				EndAction();
			}

			m_timeSinceLastSample += Time.deltaTime;

			if (m_timeSinceLastSample < sampleInterval) { return; }

			destination.value = target.value.position;

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