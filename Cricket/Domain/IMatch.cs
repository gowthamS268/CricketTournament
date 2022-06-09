using System.Text.Json.Serialization;
using Cricket.Enums;
using JOIEnergy.Domain;

namespace Cricket.Domain;

public interface IMatch
{
    public long Id { get; }
    public string Name { get; }
    public long TeamOne { get; }
    public long TeamTwo { get; }
    
    [JsonIgnore]
    public MatchResult Result { get; }
    
    [JsonIgnore]
    public string ResultSummary { get; }
    
    [JsonIgnore]
    public List<Innings> Innings { get; }
    public Innings? CreateInnings(InningsParameter inningsParam, bool isSuperOver);
}