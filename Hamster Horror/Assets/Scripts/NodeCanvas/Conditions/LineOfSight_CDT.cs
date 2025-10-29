using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Conditions
{

	public class LineOfSight_CDT : ConditionTask
	{

		public BBParameter<Transform> target;
		public BBParameter<LayerMask> blockVisionLayers;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit()
		{
			if (!target.useBlackboard)
			{
				Debug.LogError("Target must be a blackboard variable");
			}

			return null;
		}

		//Called whenever the condition gets enabled.
		protected override void OnEnable()
		{

		}

		//Called whenever the condition gets disabled.
		protected override void OnDisable()
		{

		}

		//Called once per frame while the condition is active.
		//Return whether the condition is success or failure.
		protected override bool OnCheck()
		{
			Vector3 vectorToTarget = target.value.position - agent.transform.position;

			return Physics.Raycast(agent.transform.position, vectorToTarget, vectorToTarget.magnitude, blockVisionLayers.value);
		}
	}
}