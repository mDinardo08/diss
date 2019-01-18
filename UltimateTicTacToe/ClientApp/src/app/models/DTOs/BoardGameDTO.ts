import { Player } from "../player/player.model";
import { Move } from "../move/move.model";
import { BoardGame } from "../boardGame/boardgame/boardgame.model";

export class BoardGameDTO {
    public game: Array<Array<BoardGame>>;
    public Winner: Player;
    public cur: Player;
    public players: Array<Player>;
    public lastMove: Move;
}
