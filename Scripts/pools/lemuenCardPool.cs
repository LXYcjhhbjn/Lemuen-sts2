using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace LXY.Scripts;
public class lemuenCardPool: CustomCardPoolModel
{
    public override string Title => "lemuen";
    public override string? TextEnergyIconPath => "res://lemuen/images/energy_test.png";
    public override string? BigEnergyIconPath => "res://lemuen/images/energy_test_big.png";
    public override Texture2D? CustomFrame(CustomCardModel card)
    {
        return card.Type switch
        {
            CardType.Attack => PreloadManager.Cache.GetAsset<Texture2D>("res://lemuen/images/card_frame_attack.png"),
            CardType.Power => PreloadManager.Cache.GetAsset<Texture2D>("res://lemuen/images/card_frame_power.png"),
            _ => PreloadManager.Cache.GetAsset<Texture2D>("res://lemuen/images/card_frame_skill.png"),
        };
    }
    public override Color DeckEntryCardColor => new(1f, 1f, 1f);
    public override bool IsColorless => false;
}