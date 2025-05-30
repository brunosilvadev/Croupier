using Croupier.Model;

namespace Croupier.Workers;

public class SessionManager : ISessionManager
{
    private readonly List<Session> _sessions;
    public SessionManager()
    {
        _sessions = new();
    }
    public string NewSession(int? numberOfDecks)
    {
        _sessions.Add(new Session(numberOfDecks ?? 1));
        return _sessions.Last().SessionId;
    }
    public Session? GetSession(string sessionId)
    {
        return _sessions.FirstOrDefault(s => s.SessionId == sessionId);
    }
    public Card? DrawCard(string sessionId)
    {
        var deck = _sessions.FirstOrDefault(s => s.SessionId == sessionId)?.Cards.Cards;
        if (deck == null || deck.Count == 0)
            return null;
        return deck.Pop();
    }
    public Stack<Card>? SeeDeck(string sessionId)
    {
        return _sessions.FirstOrDefault(s => s.SessionId == sessionId)?
                .Cards.Cards;
    }
    public Stack<Card>? ShuffleDeck(string sessionId)
    {
        var deck = _sessions.FirstOrDefault(s => s.SessionId == sessionId)?
                    .Cards;

        if (deck != null)
        {
            deck.Cards = Shuffler.Shuffle(deck.Cards);
            return deck.Cards;
        }
        return null;        
    }
}
