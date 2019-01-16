import { Player } from "../../models/player/player.model";

export interface BoardGameComponent {

    setBoard(board: Array<Array<BoardGameComponent>>): void;

    setOwner(owner: Player): void;
}
