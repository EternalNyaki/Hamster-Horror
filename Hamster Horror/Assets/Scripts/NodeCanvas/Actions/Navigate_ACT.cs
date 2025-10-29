using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;

namespace NodeCanvas.Tasks.Actions
{

	public class Navigate_ACT : ActionTask
	{
		public BBParameter<Vector3> targetPosition;
		public float sampleInterval;
		public float sampleRadius;

		private NavMeshAgent _navAgent;
		private float _timeSinceLastSample;
		private Vector3 _lastTarget = Vector3.zero;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit()
		{
			_navAgent = agent.GetComponent<NavMeshAgent>();

			targetPosition.value = agent.transform.position;

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
			_timeSinceLastSample += Time.deltaTime;
			Debug.DrawLine(agent.transform.position, _navAgent.destination, Color.red);

			if (_timeSinceLastSample < sampleInterval) { return; }

			if (_lastTarget != targetPosition.value)
			{
				NavMeshHit navMeshHit;
				if (NavMesh.SamplePosition(targetPosition.value, out navMeshHit, sampleRadius, NavMesh.AllAreas))
				{
					_navAgent.SetDestination(navMeshHit.position);
					_lastTarget = targetPosition.value;
				}
			}

			_timeSinceLastSample -= sampleInterval;
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