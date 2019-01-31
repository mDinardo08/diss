import { PlayerColour } from "../../player/player.colour.enum";

export class BoardGame {

    public board: Array<Array<BoardGame>>;
    public owner: PlayerColour;
}
