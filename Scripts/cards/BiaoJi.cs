using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class BiaoJi: CustomCardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<AIM>(2m),new PowerVar<TongJi>(2m)];
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
		HoverTipFactory.FromPower<AIM>(),
        HoverTipFactory.FromPower<TongJi>()
    ];	
	public BiaoJi()
		: base(0, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy , true)
	{
    }
	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
		await PowerCmd.Apply<AIM>(choiceContext , base.Owner.Creature, base.DynamicVars["AIM"].BaseValue, base.Owner.Creature, this);
        await PowerCmd.Apply<TongJi>(choiceContext , cardPlay.Target, base.DynamicVars["TongJi"].BaseValue, base.Owner.Creature, this);
	}
	protected override void OnUpgrade()
	{
        base.DynamicVars["AIM"].UpgradeValueBy(2);
	}
}