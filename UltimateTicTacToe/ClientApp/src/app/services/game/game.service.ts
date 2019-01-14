import { Injectable } from "@angular/core";
import { ApiService } from "../api/api.service";
import { Boardgame } from "../../models/boardGame/boardgame.model";
import { BoardGameDTO } from "../../models/DTOs/tictactoeDTO";
import { Observable } from "rxjs/Observable";

@Injectable()
export class GameService {

    constructor(private api: ApiService) {}


    makeMove(game: Array<Array<Boardgame>>): Observable<BoardGameDTO> {
        return this.api.post<BoardGameDTO>("makeMove", game);
    }

}
