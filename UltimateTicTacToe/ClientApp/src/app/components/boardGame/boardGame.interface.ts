import { BoardGame } from "../../models/boardGame/boardgame/boardgame.model";

export interface BoardGameComponent {

    setBoard(board: BoardGame): void;
}
