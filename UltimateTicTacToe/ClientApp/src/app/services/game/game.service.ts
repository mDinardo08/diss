import { Injectable } from "@angular/core";
import { ApiService } from "../api/api.service";
import { BoardGameDTO } from "../../models/DTOs/BoardGameDTO";
import { Observable } from "rxjs/Observable";
import { BoardGame } from "../../models/boardGame/boardgame/boardgame.model";

@Injectable()
export class GameService {


    constructor(private api: ApiService) {}

    makeMove(game: Array<Array<BoardGame>>): Observable<BoardGameDTO> {
        return this.api.post<BoardGameDTO>("Game/makeMove", game);
    }

    createGame(size: number): Observable<BoardGame> {
        return this.api.get<BoardGame>("Game/createBoard/" + size);
    }
}
