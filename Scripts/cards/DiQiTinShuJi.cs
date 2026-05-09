using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class DiQiTinShuJi: CustomCardModel
{
	protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<DiQiTinShuJiPower>(1m)];
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
		HoverTipFactory.FromPower<DiQiTinShuJiPower>(),
		HoverTipFactory.FromPower<PlatingPower>()
    ];
	public DiQiTinShuJi()
		: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
	{
	}
	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
		await PowerCmd.Apply<DiQiTinShuJiPower>(choiceContext , base.Owner.Creature, base.DynamicVars["DiQiTinShuJiPower"].BaseValue, base.Owner.Creature, this);
	}
	protected override void OnUpgrade()
	{
		base.DynamicVars["DiQiTinShuJiPower"].UpgradeValueBy(1m);
	}
}