using GameStoreApi.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//list of games hardcoded
List<GameDto> games = [
    new(
        1,
        "PubG",
        "War",
        19.9M,
        new DateOnly(2015, 7, 17)),

    new(
        2,
        "MLBB",
        "Fighting",
        12.89M,
        new DateOnly(2010, 9, 30)),

    new(
        3,
        "Freefire",
        "Fire",
        13.99M,
        new DateOnly(2015, 9, 21))
];


//GET /games
app.MapGet("games", () => games);

//GET /games/1
app.MapGet("games/{id}", (int id) => games.Find(game => game.Id == id));

//POST /games
app.MapPost("games", (CreateGameDto newGame) =>
{

});

app.Run();
