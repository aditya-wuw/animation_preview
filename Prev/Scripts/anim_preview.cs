using Godot;
using System;
using System.Linq;

public partial class anim_preview : CanvasLayer
{
   const string smugCat = @"
       /\                ______
      /  \______________/      \
     /                          \
    /    O                 O     \____
   |            \    /                |
   |   @         \/\/       @        /
   |                                /
   |_______________________________/  Null Team's :3
";
   [Export] private Node3D Model;
   [Export] private PackedScene BtnScene;
   [Export] private VBoxContainer AnimContainer;
   [Export] private LineEdit SearchBar;
   private string[] Anims;
   private AnimationPlayer AnimPlayer;
   public override void _Ready()
   {
	  GD.Print(smugCat);
	  SearchBar.TextChanged += SearchTerms;
	  AnimPlayer = Model.GetNode<AnimationPlayer>("AnimationPlayer");
	  Anims = AnimPlayer.GetAnimationList();
	  DisplayAnimations(Anims);
   }

   private void DisplayAnimations(string[] displayList)
   {
	  AnimContainer.GetChildren().OfType<Button>().ToList().ForEach(b => b?.QueueFree());
	  foreach (string anim in displayList)
	  {
		 Button newBTN = BtnScene.Instantiate<Button>();
		 newBTN.Text = anim;
		 newBTN.Pressed += () => PlayAnimation(anim);
		 AnimContainer.AddChild(newBTN);
	  }
   }


   private void SearchTerms(string Terms)
   {
	  GD.Print(Terms);
	  string[] filtered = [.. Anims.Where(s => s.Contains(Terms, StringComparison.CurrentCultureIgnoreCase))];
	  DisplayAnimations(filtered);
   }

   private void PlayAnimation(string anim)
   {
	  AnimPlayer.Play(anim);
   }


}
