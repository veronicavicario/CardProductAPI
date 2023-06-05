using CardProductAPI.Models;
using CardProductAPI.Models.Data;

namespace CardProductAPI.Services;

public class CardsService
{
    private readonly CardProductContext _context;

    public CardsService(CardProductContext context)
    {
        _context = context;
    }
    public List<Card> GetAllByUserId(long UserId)
    {
        return _context.Cards.Where(c => c.UserId == UserId).ToList();
    }
    
    public void Save(Card card)
    {
         _context.Cards.Add(card);
         _context.SaveChanges();
    }
}