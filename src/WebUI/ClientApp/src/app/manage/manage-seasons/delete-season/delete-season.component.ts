import {Component, OnInit} from '@angular/core';
import {SeasonDto, SeasonsClient} from "../../../web-api-client";
import {ActivatedRoute, Router} from "@angular/router";
import {Constants} from "../../../../assets/constants";

@Component({
  selector: 'app-delete-season',
  templateUrl: './delete-season.component.html',
  styleUrls: ['./delete-season.component.css']
})
export class DeleteSeasonComponent implements OnInit {
  seasonId: number;
  season: SeasonDto;

  constructor(
    private currentRoute: ActivatedRoute,
    private seasonsClient: SeasonsClient,
    private router: Router
  ) {}

  ngOnInit() {
    this.seasonId = Number(this.currentRoute.snapshot.paramMap.get(Constants.IdParameter));
    this.seasonsClient.get(this.seasonId).subscribe(
      result => {
        this.season = result;
      },
      error => console.error(error)
    );
  }

  onSubmit(): void {
    this.seasonsClient.delete(
      this.seasonId
    ).subscribe(
      result => {
        this.router.navigateByUrl(Constants.ManageMediasRoute + '/' + this.season.mediaId);
      },
      error => console.error(error)
    );
  }
}

