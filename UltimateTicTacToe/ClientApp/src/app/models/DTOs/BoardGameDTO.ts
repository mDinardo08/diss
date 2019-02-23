import { Player } from "../player/player.model";
import { Move } from "../move/move.model";
import { BoardGame } from "../boardGame/boardgame/boardgame.model";
import { PlayerColour } from "../player/player.colour.enum";

export class BoardGameDTO {
    public game: Array<Array<BoardGame>>;
    public winner: PlayerColour;
    public cur: Player;
    public players: Array<Player>;
    public lastMove: Move;
    public availableMoves: Array<Move>;
    public lastMoveRating: number;
    public highOption: number;
    public lowOption: number;
}
