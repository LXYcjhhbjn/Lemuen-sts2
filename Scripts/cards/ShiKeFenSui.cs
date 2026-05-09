using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class ShiKeFenSui: CustomCardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<AMMO>(1m)];
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
		HoverTipFactory.FromPower<AMMO>(),
		HoverTipFactory.FromPower<FenSuiDan>(),
		HoverTipFactory.FromPower<BaoPoDan>(),
		HoverTipFactory.FromCard<FenSuiZhuangTian>(),
		HoverTipFactory.FromCard<BaoPoZhuangTian>()
    ];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
	public ShiKeFenSui()
		: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self,true)
	{
    }
	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		await PowerCmd.Apply<AMMO>(choiceContext,base.Owner.Creature, base.DynamicVars["AMMO"].BaseValue, base.Owner.Creature, this);
		List<CardModel>cards= new List<CardModel> {base.CombatState.CreateCard<FenSuiZhuangTian>(base.Owner),base.CombatState.CreateCard<BaoPoZhuangTian>(base.Owner)};
		CardModel cardModel = await CardSelectCmd.FromChooseACardScreen(choiceContext, cards, base.Owner, canSkip: false);
		if (cardModel != null)
		{
			if (cardModel is FenSuiZhuangTian)
			{
				await PowerCmd.Apply<FenSuiDan>(choiceContext,base.Owner.Creature, 1, base.Owner.Creature, this);
			}
			else
			{
				await PowerCmd.Apply<BaoPoDan>(choiceContext,base.Owner.Creature, 1, base.Owner.Creature, this);
			}
		}
	}
	protected override void OnUpgrade()
	{
		base.DynamicVars["AMMO"].UpgradeValueBy(1m);
	}
}
