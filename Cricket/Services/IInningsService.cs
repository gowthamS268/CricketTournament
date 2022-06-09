using System.Net.Mime;
using Cricket.Domain;
using JOIEnergy.Domain;

namespace Cricket.Services;

public interface IInningsService
{
    public List<IMatch> Matches { get; }    
    public Innings? Create(InningsParameter inningsParam, IMatch match);

    public Innings? CreateSuperOver(InningsParameter superOverParams, IMatch match);
    public string PlaySuperOver(SuperOverParameter superOverParams, Innings innings);
}