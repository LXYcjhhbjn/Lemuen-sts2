using Godot;

public partial class defaultskle : BaseButton
{
	public static int SkinIndex = 0 ;
	public override void _Ready()
	{
		Pressed += OnPressed;
	}

	public void OnPressed()
	{
		SkinIndex = (SkinIndex + 1) % 2;
	}
}
