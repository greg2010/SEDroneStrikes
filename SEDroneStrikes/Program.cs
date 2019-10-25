using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using VRage.Collections;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ObjectBuilders.Definitions;
using VRage.Game;
using VRage;
using VRageMath;


namespace IngameScript
{
    partial class Program : MyGridProgram
    {
        // This file contains your actual script.
        //
        // You can either keep all your code here, or you can create separate
        // code files to make your program easier to navigate while coding.
        //
        // In order to add a new utility class, right-click on your project, 
        // select 'New' then 'Add Item...'. Now find the 'Space Engineers'
        // category under 'Visual C# Items' on the left hand side, and select
        // 'Utility Class' in the main area. Name it in the box below, and
        // press OK. This utility class will be merged in with your code when
        // deploying your final script.
        //
        // You can also simply create a new utility class manually, you don't
        // have to use the template if you don't want to. Just do so the first
        // time to see what a utility class looks like.
        // 
        // Go to:
        // https://github.com/malware-dev/MDK-SE/wiki/Quick-Introduction-to-Space-Engineers-Ingame-Scripts
        //
        // to learn more about ingame scripts.
        //import logger.cs;
        static int logSize = 30;

        private Logger logger = null;
        
        public Program()
        {
            // The constructor, called only once every session and
            // always before any other method is called. Use it to
            // initialize your script. 
            //     
            // The constructor is optional and can be removed if not
            // needed.
            // 
            // It's recommended to set Runtime.UpdateFrequency 
            // here, which will allow your script to run itself without a 
            // timer block.

            this.logger = new Logger(Echo, Me, logSize);
        }

        public void Save()
        {
            // Called when the program needs to save its state. Use
            // this method to save your state to the Storage field
            // or some other means. 
            // 
            // This method is optional and can be removed if not
            // needed.
        }
        
        //List<IMyHydrogenEngine> hydrogenEngines = new List<>();

        public void Main(string argument, UpdateType updateSource)
        {
            // The main entry point of the script, invoked every time
            // one of the programmable block's Run actions are invoked,
            // or the script updates itself. The updateSource argument
            // describes where the update came from. Be aware that the
            // updateSource is a  bitfield  and might contain more than 
            // one update type.
            // 
            // The method itself is required, but the arguments above
            // can be removed if not needed.

            Runtime.UpdateFrequency = UpdateFrequency.Update1; //runtime every second

            //remoteControlReference();

            //string textDisplay = "testText1234";
            //textDisplayFunction(textDisplay);
            //textDisplayFunction(vectorsFromReference(remoteControlReference()), "LCD Panel");
            //PistonLengthDisplay();
           
        }

        private List<T> GetBlocksByNameOfType<T>(string name)
        {
            List<IMyTerminalBlock> blocks = new List<IMyTerminalBlock>();
            List<T> casted = new List<T>();
            GridTerminalSystem.SearchBlocksOfName(name, blocks, block => block is T);
            foreach (var thruster in blocks)
            {
                casted.Add((T)thruster);
            }
            return casted;
        }

        public void PistonLengthDisplay() { 
            IMyTextPanel Display = GridTerminalSystem.GetBlockWithName("Lakeshore Base Drill LCD Panel 1") as IMyTextPanel; 
            IMyPistonBase Piston = GridTerminalSystem.GetBlockWithName("Lakeshore Base Long Piston 1") as IMyPistonBase; 
            float PistonPositionAsNumber = Piston.CurrentPosition; 
            string PistonPositionAsString = string.Format("{0:0.00}", PistonPositionAsNumber); 
            Display.WriteText("Piston 1 position: " + PistonPositionAsString);
        }

        public bool textDisplayFunction(string testText, string displayName)
        {
            IMyTextPanel Display = GridTerminalSystem.GetBlockWithName(displayName) as IMyTextPanel;
            bool displayOutput = Display.WriteText(testText);
            return displayOutput;
        }

        public IMyTerminalBlock remoteControlReference()
        {
            var list = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocksOfType<IMyRemoteControl>(list);
            var reference = list[0] as IMyRemoteControl;           
            return reference;
        }
        /*public string vectorsFromReference(IMyTerminalBlock reference)
        {
            var currentPosition = reference.Position;

            //ForwardPos - position of forward direction of block in local grid
            var forwardPos = reference.Position + Base6Directions.GetIntVector(reference.Orientation.TransformDirection(Base6Directions.Direction.Forward));
            //Forward - position of forward direction of block in global grid
            var forward = reference.CubeGrid.GridIntegerToWorld(forwardPos);
            //forwardVector - Vector (global grid) direction of forward position of reference block
            var forwardVector = Vector3D.Normalize(forward - reference.GetPosition());


            logger.Log(forwardVector.ToString());

            string remoteControlReferenceOut = ("currentPosition =\n" + currentPosition + "\nforwardPos =\n" + forwardPos + "\nForward =\n" + forward + "\nForward Vector:\n" + forwardVector);
            return remoteControlReferenceOut;
        }

        public void positionReference()
        {
            var list = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocksOfType<IMyThrust>(list);
            var referenceThrust  = list[0] as IMyThrust;

        }
        public IMyTerminalBlock thrusterAssigner(IMyTerminalBlock reference)
        {

            var thrusters = new Dictionary<Vector3, List<IMyThrust>>();

            List<IMyTerminalBlock> blocks = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocksOfType<IMyThrust>(blocks);

            Matrix fromGridToReference;
            reference.Orientation.GetMatrix(out fromGridToReference);
            Matrix.Transpose(ref fromGridToReference, out fromGridToReference);
            for (int i = 0; i < blocks.Count; ++i)
            {
                IMyThrust thruster = blocks[i] as IMyThrust;
                Matrix fromThrusterToGrid;
                thruster.Orientation.GetMatrix(out fromThrusterToGrid);
                Vector3 accelerationDirection = Vector3.Transform(fromThrusterToGrid.Backward, fromGridToReference);

                if (!thrusters.ContainsKey(accelerationDirection))
                    thrusters[accelerationDirection] = new List<IMyThrust>();
                thrusters[accelerationDirection].Add(thruster);
            }
            return blocks;
        }*/


    }
}
