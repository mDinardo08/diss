import { Player } from "../player/player.model";
import { Move } from "../move/move.model";

export class BoardGameDTO {
    public game: [[any]];
    public Winner: Player;
    public next: number;
    public lastMove: Move;
}
