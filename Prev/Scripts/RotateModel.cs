using Godot;
using System;

public partial class RotateModel : Node3D
{
	[Export] public Node3D Model;
	[Export] public float Sensitivity = 0.5f;
	[Export] public float Smoothness = 10.0f;
	[Export] public bool InvertDrag = true;

	private bool _isDragging = false;
	private float _targetRotationY = 0.0f;

	public override void _Ready()
	{
		if (Model != null)
			_targetRotationY = Model.Rotation.Y;
	}

	public override void _UnhandledInput(InputEvent @event)
	{

		if (@event is InputEventMouseButton mouseButton)
		{
			if (mouseButton.ButtonIndex == MouseButton.Left)
			{
				_isDragging = mouseButton.Pressed;
			}
		}

		if (_isDragging && @event is InputEventMouseMotion mouseMotion)
		{
			_targetRotationY -= Mathf.DegToRad(mouseMotion.Relative.X * Sensitivity * (InvertDrag ? -1 : 1));
		}
	}

	public override void _Process(double delta)
	{
		if (Model == null) return;
		Vector3 currentRotation = Model.Rotation;
		currentRotation.Y = Mathf.LerpAngle(currentRotation.Y, _targetRotationY, (float)delta * Smoothness);
		Model.Rotation = currentRotation;
	}
}
