
//Include code
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//This is Generate code
public class ResourceLoader 
{
    private ResourceRequest[] requests;

		public Texture2D _4_2 { get; private set; }
		public Texture2D noise2 { get; private set; }
		public GameObject VertexColor { get; private set; }
		public Texture2D _4_1 { get; private set; }
		public Texture2D fireTest { get; private set; }
		public Texture2D dot { get; private set; }

	public float Progress { get { return this.requests.Average(r => r.progress); } }
	public bool IsDone { get { return this.requests.All(r => r.isDone); } }

	public ResourceLoader()
	{
		this.requests = new ResourceRequest[6];
		this.requests[0] = Resources.LoadAsync("4_2.tga", typeof(Texture2D));
		this.requests[1] = Resources.LoadAsync("noise2.png", typeof(Texture2D));
		this.requests[2] = Resources.LoadAsync("VertexColor.FBX", typeof(GameObject));
		this.requests[3] = Resources.LoadAsync("4_1.tga", typeof(Texture2D));
		this.requests[4] = Resources.LoadAsync("fireTest.tga", typeof(Texture2D));
		this.requests[5] = Resources.LoadAsync("dot.tga", typeof(Texture2D));
	}
}

