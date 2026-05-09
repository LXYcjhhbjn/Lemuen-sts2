using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class DiaoPei: CustomCardModel
{
    protected override bool ShouldGlowGoldInternal => IsPlayable;
    protected override bool IsPlayable => Owner.Creature.HasPower<AMMO>();
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
	protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<AMMO>(),HoverTipFactory.FromKeyword(MyKeywords.Modification)];	
    public DiaoPei() : base(0 , CardType.Skill , CardRarity.Common , TargetType.Self ,true)
    {
    }
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        List<CardModel> cardModel = CardFactory.GetForCombat(base.Owner.Creature.Player, from c in ModelDb.CardPool<TokenCardPool>().GetUnlockedCards(base.Owner.Creature.Player.UnlockState, base.Owner.Creature.Player.RunState.CardMultiplayerConstraint)
			where c.Keywords.Contains(MyKeywords.Modification)
			select c, base.Owner.Creature.GetPowerAmount<AMMO>() >> 1 , base.Owner.RunState.Rng.CombatCardGeneration).ToList();
		if (cardModel != null)
		{
		    IReadOnlyList<CardPileAddResult> results = await CardPileCmd.AddGeneratedCardsToCombat(cardModel, PileType.Exhaust, cardPlay.Card.Owner);
			CardCmd.PreviewCardPileAdd(results);
        }
        await PowerCmd.Remove<AMMO>(base.Owner.Creature);
    }
    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Exhaust);
    }
}