using Z3.Audio.FMODIntegration;
using Z3.NodeGraph.Tasks;
using Z3.NodeGraph.Core;
using UnityEngine;
using Z3.Utils.ExtensionMethods;
using Z3.DemoSkull.Shared;

namespace Z3.DemoSkull.NodeGraph.Audio
{
    [NodeCategory(MenuPath.Audio)]
    [NodeDescription("Play the current SoundData")]
    public class PlaySound : ActionTask
    {
        [ParameterDefinition(AutoBindType.SelfBind)]
        [SerializeField] private Parameter<Transform> data;

        [SerializeField] private Parameter<SoundData> soundData;
        [SerializeField] private Parameter<SoundInstance> instanceReturned;

        public override string Info => !soundData.IsBinding && soundData.Value != null
            ? $"♫ Play <b>{soundData.Value.name.StringReduction()}</b>"
            : $"♫ Play <b>{soundData}</b>" ;

        protected override void StartAction()
        {
            instanceReturned.Value = soundData.Value.PlaySound(data.Value);
            EndAction();
        }
    }
}