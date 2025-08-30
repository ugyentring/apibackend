using Microsoft.EntityFrameworkCore;

namespace GameStoreApi.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options)
 : DbContext(options)
{

}
