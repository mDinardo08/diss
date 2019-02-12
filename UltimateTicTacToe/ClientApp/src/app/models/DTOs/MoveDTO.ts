import { Move } from "../move/move.model";
import { BoardGame } from "../boardGame/boardgame/boardgame.model";

export class MoveDTO {
    public move: Move;
    public game: Array<Array<BoardGame>>;
    public UserId: number;
}
