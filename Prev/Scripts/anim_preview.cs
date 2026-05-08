using Godot;
using System;

public partial class anim_preview : CanvasLayer
{
	public override void _Ready()
	{
		string smugCat = @"
       /\                ______
      /  \______________/      \
     /                          \
    /    O                 O     \____
   |            \    /                |
   |   @         \/\/       @        /
   |                                /
   |_______________________________/  Null Team's :3
";
		GD.Print(smugCat);
	}

	public override void _Process(double delta)
	{
	}
}
