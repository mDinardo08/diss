import { BoardGameComponent } from "../boardGame/boardGame.interface";
import { Player } from "../../models/player/player.model";

export abstract class AbstractBoardGameComponent implements BoardGameComponent {

    owner: Player;

    abstract setBoard(board: BoardGameComponent[][]): void;

    setOwner(owner: Player): void {
        this.owner = owner;
    }


}
