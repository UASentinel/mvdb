import {Component, OnInit} from '@angular/core';
import {MediasClient, SeasonDto, SeasonsClient} from "../../web-api-client";
import {DomSanitizer, SafeResourceUrl} from "@angular/platform-browser";
import {ActivatedRoute} from "@angular/router";
import {AuthorizeService} from "../../../api-authorization/authorize.service";

@Component({
  selector: 'app-view-season',
  templateUrl: './view-season.component.html',
  styleUrls: ['./view-season.component.css']
})
export class ViewSeasonComponent implements OnInit {
  seasonId: number;
  season: SeasonDto;
  trailerLink: SafeResourceUrl;
  duration: string;
  isAdministrator: boolean = false;

  constructor(
    private currentRoute: ActivatedRoute,
    private mediasClient: MediasClient,
    private seasonsClient: SeasonsClient,
    private sanitizer: DomSanitizer,
    private authorizeService: AuthorizeService
  ) {}

  ngOnInit() {

    this.seasonId = Number(this.currentRoute.snapshot.paramMap.get('id'));

    this.seasonsClient.get(this.seasonId).subscribe(
      result => {
        this.season = result;

        let hours = Math.floor(this.season.duration / 60);
        let minutes = this.season.duration % 60;

        if(hours === 0)
          this.duration = minutes + 'm';
        else
          this.duration = hours + 'h ' + minutes + 'm';

        this.season.episodes.forEach(e => {
          let hours = Math.floor(e.duration / 60);
          let minutes = e.duration % 60;

          if(hours === 0)
            e['durationString'] = minutes + 'm';
          else
            e['durationString'] = hours + 'h ' + minutes + 'm';
        })

        this.trailerLink = this.sanitizer.bypassSecurityTrustResourceUrl(this.season.trailerLink);
      },
      error => console.error(error)
    );

    this.authorizeService.getUserRoles().subscribe(
      roles => {
        this.isAdministrator = roles && roles.findIndex(r => r === 'Administrator') !== -1;
      }
    );
  }
}
