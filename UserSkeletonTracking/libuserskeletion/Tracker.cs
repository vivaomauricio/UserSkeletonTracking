using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using OpenNI;
using NITE;

namespace libuserskeletion
{
	public class Tracker
	{
		
		private string config_xml;
		private Context context;
		private ScriptNode scriptNode;
		private DepthGenerator depth;
		private DepthMetaData depthMetaData;
		
		private UserGenerator userGenerator;
		private SkeletonCapability skeletonCapability;
		private PoseDetectionCapability poseDetectionCapability;
		private Dictionary<int, Dictionary<SkeletonJoint, SkeletonJointPosition>> joints;
		private string calibPose;
		
		private Thread readThread;
		private bool shouldRun;
		
		public Tracker (string config_xml)
		{
			try 
			{
				this.config_xml = config_xml;
				this.context = Context.CreateFromXmlFile(this.config_xml, out scriptNode);
				this.depth = this.context.FindExistingNode(NodeType.Depth) as DepthGenerator;
				this.depthMetaData = new DepthMetaData();
				
				if (this.depth == null)
				{
					throw new Exception(@"Error in " 
					                    + Path.GetFullPath(this.config_xml) 
					                    +  ". No depth node found.");
				}
				
				this.userGenerator = new UserGenerator(this.context);
				this.skeletonCapability = this.userGenerator.SkeletonCapability;
				this.poseDetectionCapability = this.userGenerator.PoseDetectionCapability;
				this.calibPose = this.skeletonCapability.CalibrationPose;
				
				this.shouldRun = true;
				this.readThread = new Thread(readerThread);
				this.readThread.Start();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				this.shouldRun = false;
			}		
		}
		
		private unsafe void readerThread()
		{
			
		}
	}
}

