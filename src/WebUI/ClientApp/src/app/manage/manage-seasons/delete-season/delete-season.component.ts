import {Component, OnInit} from '@angular/core';
import {SeasonDto, SeasonsClient} from "../../../web-api-client";
import {ActivatedRoute, Router} from "@angular/router";

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
    this.seasonId = Number(this.currentRoute.snapshot.paramMap.get('id'));
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
        this.router.navigateByUrl('manage/medias/' + this.season.mediaId);
      },
      error => console.error(error)
    );
  }
}

