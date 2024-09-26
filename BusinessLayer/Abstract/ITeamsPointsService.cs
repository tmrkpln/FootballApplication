using EntityLayer.Entites;
using BusinessLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ITeamsPointsService : IGenericService<TeamPoints>
    {
        void TWinnerTeam(int teamId, int TeamGolCount, int OtherTeamGolCount);
        void TLosingTeam(int teamId, int TeamGolCount, int OtherTeamGolCount);
        void TTie(int HomeOwnerId, int DisplacementID, int HomeOwnerGolCount, int DisplacemenGolCount);
        TeamPoints TGetTeamPoints(int TeamId);
        void ScoreAdd(int HomeOwnerID, int DisplacementID, int homeTeamScore, int awayTeamScore);
        (MatchDto, List<MatchResultDto>) CreateScore(MatchDates matchDates);
        int TeamScoreResult(List<MatchResultDto> matchResults);
        List<MatchResultDto> TeamScore(List<string> attackTypes, int attackRate, int chanceRate, string TeamName);
        (int, string) AttackResult(string attackType, int chanceRate);
        List<MatchResultDto> TotalList(List<MatchResultDto> matchResultsHome, List<MatchResultDto> matchResultsAway);
    }
}
