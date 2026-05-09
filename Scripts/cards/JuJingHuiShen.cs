using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class JuJIngHuiShen: CustomCardModel, ITranscendenceCard
{
	public CardModel GetTranscendenceTransformedCard() => ModelDb.Card<XinLiu>();
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<AIM>(6m),new PowerVar<ZhuanZhu>(2m)];
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
	protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<AIM>()];	
	public JuJIngHuiShen()
		: base(1, CardType.Skill, CardRarity.Basic, TargetType.Self,true)
	{
    }
	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		await PowerCmd.Apply<ZhuanZhu>(choiceContext , base.Owner.Creature, base.DynamicVars["ZhuanZhu"].BaseValue, base.Owner.Creature, this);
		await PowerCmd.Apply<AIM>(choiceContext , base.Owner.Creature, base.DynamicVars["AIM"].BaseValue, base.Owner.Creature, this);
	}

	protected override void OnUpgrade()
	{
		base.EnergyCost.UpgradeBy(-1);
	}
}
