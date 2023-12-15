using Godot;
using System;

public partial class Project1 : Node3D
{
    MeshInstance3D anchor;

    MeshInstance3D ball;

    SpringModel spring;

	Label keLabel;

	PendSim pend;

    double xA, yA, zA; //coords of anchor
    float length; //length of pedulum
    double angle; //pedulum angle
    double angleInit; //initial pedulum angle 
    double time;
    Vector3 endA; //end point for anchor
    Vector3 endB; // end point for pendulum bob


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GD.Print("HELLO MEE381 IN GODOT!");
        xA = 0.0; yA = 1.2; zA = 0.0;
        anchor = GetNode<MeshInstance3D>("Anchor");
        ball = GetNode<MeshInstance3D>("Ball1");
        spring = GetNode<SpringModel>("SpringModel");
        endA = new Vector3((float)xA, (float)yA, (float)zA);
        anchor.Position = endA;

		keLabel = GetNode<Label>("KElabel");


		pend = new PendSim();

		length = 0.9f;
        spring.GenMesh(0.05f, 0.015f, length, 6.0f,62);


        angleInit = Mathf.DegToRad(60.0);
        float angleF = (float) angleInit;
		pend.Angle = (double)angleInit;

        endB.X = endA.X + length*MathF.Sin(angleF);
        endB.Y = endA.Y - length*Mathf.Cos(angleF);
        endB.Z = endA.Z;
        PlacePendulum(endB);
        //PlacePendulum((float)angle);
        
        time = 0.0;

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        //float angleF = 1.0f*(float)Math.Sin(2.0 * time);
		//float angleA = (float)(0.4*time);
		// length = lnegth0 + 0.3f * (float)Math.Cos(4.0 * time);
		float angleA = 0.0f;

		float angleF = (float)pend.Angle;
		keLabel.Text = angleF.ToString("0.00");




		float hz = length*Mathf.Sin(angleF);
        endB.X = endA.X + hz*MathF.Sin(angleA);
        endB.Y = endA.Y - length*Mathf.Cos(angleF);
        endB.Z = endA.Z + hz*Mathf.Sin(angleA);
        PlacePendulum(endB);
        time += delta;

    }
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

		pend.StepRK2(time, delta);


    }

    //public override void _PhysicsProcess(double delta)
    //{

    //}

    private void PlacePendulum(Vector3 endBB)
    {

		spring.PlaceEndPoints(endA, endB);
        ball.Position = endBB;
    }
}