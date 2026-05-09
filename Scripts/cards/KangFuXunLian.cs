using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class KangFuXunLian: CustomCardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<ZhuanZhu>(1m),
        new PowerVar<StrengthPower>(1m),
        new PowerVar<DexterityPower>(1m)
        ];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
	protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<ZhuanZhu>(),HoverTipFactory.FromPower<StrengthPower>(),HoverTipFactory.FromPower<DexterityPower>()];	
	public KangFuXunLian()
		: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self,true)
	{
    }
	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
        await PowerCmd.Apply<ZhuanZhu>(choiceContext , base.Owner.Creature, base.DynamicVars["ZhuanZhu"].BaseValue, base.Owner.Creature, this);
		await PowerCmd.Apply<StrengthPower>(choiceContext , base.Owner.Creature, base.DynamicVars["StrengthPower"].BaseValue, base.Owner.Creature, this);
		await PowerCmd.Apply<DexterityPower>(choiceContext , base.Owner.Creature, base.DynamicVars["DexterityPower"].BaseValue, base.Owner.Creature, this);
	}
	protected override void OnUpgrade()
	{
		base.DynamicVars["StrengthPower"].UpgradeValueBy(1m);
        base.DynamicVars["DexterityPower"].UpgradeValueBy(1m);
	}
}
