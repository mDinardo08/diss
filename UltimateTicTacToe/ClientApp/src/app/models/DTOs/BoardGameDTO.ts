import { Player } from "../player/player.model";
import { Move } from "../move/move.model";
import { Boardgame } from "../boardGame/boardgame.model";

export class BoardGameDTO {
    public game: Array<Array<Boardgame>>;
    public Winner: Player;
    public next: number;
    public lastMove: Move;
}
