using GameStoreApi.Dtos;
namespace GameStoreApi.Endpoints;

public static class GamesEndPoints
{
    const string GetGameEndPointName = "GetGame";

    //list of games hardcoded
    private static readonly List<GameDto> games = [
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

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {

        var group = app.MapGroup("games");

        //GET /games
        group.MapGet("/", () => games);


        //GET /games/1
        group.MapGet("/{id}", (int id) =>
        {
            GameDto? game = games.Find(game => game.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);

        }).WithName(GetGameEndPointName);


        //POST /games
        group.MapPost("/", (CreateGameDto newGame) =>
        {
            GameDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );
            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id }, game);
        }).WithParameterValidation();

        //PUT (update) /games
        group.MapPut("/{id}", (int id, UpdateGameDto updateGameDto) =>
        {
            var index = games.FindIndex(game => game.Id == id);
            if (index == -1)
            {
                return Results.NotFound();
            }

            games[index] = new GameDto(
                id,
                updateGameDto.Name,
                updateGameDto.Genre,
                updateGameDto.Price,
                updateGameDto.ReleaseDate
            );
            return Results.NoContent();
        });


        // DELETE /games/1
        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });
        return group;
    }

}
