# Cricket Tournament

Api Project for CricketTournament

# Welcome to Cricket Tournament

Everest Engineering wants to host their first ever Internal T20 Tournament with their
Indian and Australian Teams - An event of Unison, CricSummit
is at the intersection of flairs for Coding, Data Science and Cricket.

# Introducing Cricket Api

Cricket Api provides variouys api services to conduct cricket match between two given teams at any point of time. System will have predefined set of outcomes based on the following parameters

1. Bowling Type
2. Shot Type
3. Timing of the Shot

Bowling Options:
Bouncer | InSwinger | OutSwinger | OffCutter | LegCutter | SlowerBall | Pace | Doosra, Yorker | OffBreak

Batting Option:
Straight | Flick | LegLance | LongOn | SquareCut | Sweep | CoverDrive | Pull | Scoop | UpperCut

Shot Timing:
Early | Good | Perfect | Late

System will generate shot outcomes for every balls based on the above combination. The Shot outcomes will have the appropriate commentry for the ball annlong with run scored and if any wicket is taken. Match will have list of innings and scores for every innings. Maximum number of aballs allowed for an innings is 20 and maximum alowed wickets is 10.

## Super Over

In case of equal score the match will be considered as Tied and move to super over. The second batting team will be provided with a Target to reach and list of bowlking and timing options. The chasing team will be considered as win when they reach the target withing 6 balls and maximum 2 wickets are allowed.

## API

Below is a list of API endpoints with their respective input and output. Please note that the application needs to be running. For more information about how to run the application, please refer to [run the application](#run-the-application) section below.

### Match

Endpoint

```
Get /matches/read
```

Example output

```json
[
  {
  "id": 1,
  "name": "Test_Match_1_Vs_2",
  "teamOne": 1,
  "teamTwo": 2
  }
...
]
```

Endpoint

```

Get /matches/read/{matchId}

```

Example output

```json
{
  "id": 1,
  "name": "Test_Match_1_Vs_2",
  "resultSummary": "Match In Progress"
}
```

Endpoint

```

Post /matches/create

```

Example output

```json
{
  "id": 1,
  "name": "Test_Match_1_Vs_2",
  "resultSummary": "Match In Progress"
}
```

Example of body

```json
{
    "Name": <name>,
    "TeamOneId": <TeamOneId>,
    "TeamTwoId":<TeamTwoId>
}
```

The above command returns 200 OK and `{}`.

Example output

```json
{
  "id": 1,
  "name": "Test_Match_1_Vs_2",
  "resultSummary": "Match In Progress"
}
```

Endpoint

```
Put /matches/create/{matchId}
```

Example of body

```json
{
    "Name": <name>,
    "TeamOneId": <TeamOneId>,
    "TeamTwoId":<TeamTwoId>
}
```

The above command returns 200 OK and `{}`.

Example output

```json
{
  "id": 1,
  "name": "Test_Match_1_Vs_2",
  "resultSummary": "Match In Progress"
}
```

### Innings

Endpoint

```
Get /matches/{matchId}/innings/read
```

Example output

```json
[
  {
    "id": 1,
    "isSuperOver": false,
    "name": "Team_A_Batting",
    "battingId": 1,
    "bowlingId": 2,
    "score": 0,
    "wickets": 0
  }
]
```

Endpoint

```
Get /matches/{matchId}/innings/read/{inningsId}
```

Example output

```json
{
  "id": 1,
  "isSuperOver": false,
  "name": "Team_A_Batting",
  "battingId": 1,
  "bowlingId": 2,
  "score": 0,
  "wickets": 0
}
```

Endpoint

```
Post /matches/{matchId}/innings/create
```

Example output

```json
{
  "id": 1,
  "isSuperOver": false,
  "name": "Team_A_Batting",
  "battingId": 1,
  "bowlingId": 2,
  "score": 0,
  "wickets": 0
}
```

Example of body

```json
{
     "Name": <Name>,
    "BattingTeamId":<BattingTeamId>,
    "BowlingTeamId":<BowlingTeamId>
}
```

Example output

```json
{
  "id": 1,
  "isSuperOver": false,
  "name": "Team_A_Batting",
  "battingId": 1,
  "bowlingId": 2,
  "score": 0,
  "wickets": 0
}
```

Endpoint

```
Post /matches/{matchId}/super-over/play
```

Example of body

```json
{
    "Name": <Name>,
    "BattingTeamId":<BattingTeamId>,
    "BowlingTeamId":<BowlingTeamId>,
    "Target":<Target>,
    "Shots":[
          {"BattingType":<BattingType>,"ShotTiming":<ShotTiming>},
          {"BattingType":<BattingType>,"ShotTiming":<ShotTiming>},
          {"BattingType":<BattingType>,"ShotTiming":<ShotTiming>},
          {"BattingType":<BattingType>,"ShotTiming":<ShotTiming>},
          {"BattingType":<BattingType>,"ShotTiming":<ShotTiming>},
          {"BattingType":<BattingType>,"ShotTiming":<ShotTiming>}
        ]
}
```

The above command returns 200 OK and `output string`.

Example output

```string
Team B Won
```

### BallOutcomes

Endpoint

```
Get /matches/{matchId}/innings/{inningsId}/balls/read
```

Example output

```json
[
  {
    "commentry": "just Over the filder",
    "run": 6,
    "isWicket": false
  }
]
```

Endpoint

```
Get /matches/{matchId}/innings/{inningsId}/balls/read/{ballId}
```

Example output

```json
{
  "commentry": "just Over the filder",
  "run": 6,
  "isWicket": false
}
```

Endpoint

```
Put /matches/{matchId}/innings/{inningsId}/balls/create/{ballId}
```

Example of body

```json
{
  "commentry": "just Over the filder",
  "run": 6,
  "isWicket": false
}
```

The above command returns 200 OK and `{}`.

Example output

```json
{
  "commentry": "just Over the filder",
  "run": 6,
  "isWicket": false
}
```

## Requirements

The project requires [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0).

## Compatible IDEs

Tested on:

- Visual Studio 2022 (17.1)
- Visual Studio for Mac (8.10)
- Visual Studio Code (1.64)

## Useful commands

From the terminal/shell/command line tool, use the following commands to build, test and run the API.

### Build the project

```console
$ dotnet build
```

### Run the tests

```console
$ dotnet test CricketTest
```

### Run the application

Run the application which will be listening on port `5000`.

```console
$ dotnet run --project Cricket
```
