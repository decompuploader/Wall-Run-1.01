using GTA;
using GTA.Math;
using GTA.Native;
using System;
using System.Windows.Forms;

namespace prova
{
  public class Class1 : Script
  {
    public Class1()
    {
      this.Tick += new EventHandler(this.OnTick);
      this.KeyDown += new KeyEventHandler(this.OnKeyDown);
      this.KeyUp += new KeyEventHandler(this.OnKeyUp);
      this.Interval = 5;
    }

    private void RotToPos(Vector3 postoface)
    {
      Vector3 vector3 = postoface;
      Vector3 position = Game.Player.Character.Position;
      Game.Player.Character.Heading = Function.Call<float>(Hash._0x2FFB6B224F4B2926, (InputArgument) (vector3.X - position.X), (InputArgument) (vector3.Y - position.Y));
    }

    public void OnTick(object sender, EventArgs e)
    {
      this.OnKeyDown();
      this.OnKeyUp();
    }

    public void OnKeyDown()
    {
      Ped character = Game.Player.Character;
      Game.DisableControlThisFrame(2, GTA.Control.Cover);
      if (!Game.IsControlPressed(2, GTA.Control.Cover))
        return;
      Function.Call<float>(Hash._0x8BB4EF4214E0E6D5, (InputArgument) Game.Player.Character);
      Function.Call<float>(Hash._0x866A4A5FAE349510, (InputArgument) Game.Player.Character);
      RaycastResult raycastResult = World.RaycastCapsule(new Vector3(character.Position.X, character.Position.Y, character.Position.Z + 1.3f), new Vector3(character.Position.X, character.Position.Y, character.Position.Z + 1.3f), 1.4f, IntersectOptions.Map | IntersectOptions.Objects | IntersectOptions.Vegetation, (Entity) Game.Player.Character);
      if (raycastResult.DitHitAnything)
      {
        if (Game.IsControlJustPressed(2, GTA.Control.Cover))
        {
          this.RotToPos(raycastResult.HitCoords);
          character.Task.ClearAllImmediately();
          character.Task.PlayAnimation("move_m@gangster@generic", "sprint", 8f, 1f, -1, AnimationFlags.Loop, 0.0f);
        }
        if (Function.Call<bool>(Hash._0x1F0B79228E461EC9, (InputArgument) character, (InputArgument) "move_m@gangster@generic", (InputArgument) "sprint", (InputArgument) 1))
        {
          Function.Call(Hash._0x9FF447B6B6AD960A, (InputArgument) character, (InputArgument) false);
          character.Rotation = new Vector3(92f, character.Rotation.Y, character.Rotation.Z);
          character.ApplyForce(new Vector3(0.0f, 0.0f, 0.2f));
        }
      }
      if (Function.Call<bool>(Hash._0x1F0B79228E461EC9, (InputArgument) character, (InputArgument) "move_m@gangster@generic", (InputArgument) "sprint", (InputArgument) 1) && !raycastResult.DitHitAnything)
      {
        character.Task.ClearAllImmediately();
        character.Task.Climb();
      }
    }

    public void OnKeyDown(object sender, KeyEventArgs e)
    {
    }

    public void OnKeyUp()
    {
      if (!Function.Call<bool>(Hash._0x1F0B79228E461EC9, (InputArgument) Game.Player.Character, (InputArgument) "move_m@gangster@generic", (InputArgument) "sprint", (InputArgument) 1) || !Game.IsControlJustReleased(2, GTA.Control.Cover))
        return;
      Game.Player.Character.Task.ClearAll();
      Function.Call(Hash._0x9FF447B6B6AD960A, (InputArgument) Game.Player.Character, (InputArgument) true);
    }

    public void OnKeyUp(object sender, KeyEventArgs e)
    {
    }
  }
}
