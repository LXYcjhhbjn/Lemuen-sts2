using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LXY.Scripts;

[Pool(typeof(lemuenPotionPool))]
public class AimPotion : CustomPotionModel
{
    public override PotionRarity Rarity => PotionRarity.Common;
    public override PotionUsage Usage => PotionUsage.CombatOnly;
    public override TargetType TargetType => TargetType.Self;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<AIM>(12m)];
    public override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<AIM>()];
    public override string? CustomPackedImagePath => "res://lemuen/images/potions/AimPotion.png";
    public override string? CustomPackedOutlinePath => "res://lemuen/images/potions/AimPotion.png";
    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        await PowerCmd.Apply<AIM>(choiceContext,base.Owner.Creature, base.DynamicVars["AIM"].BaseValue, base.Owner.Creature, null);
    }
}