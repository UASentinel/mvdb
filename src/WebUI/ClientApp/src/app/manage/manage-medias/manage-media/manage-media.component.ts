import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {MediaDto, MediasClient} from "../../../web-api-client";
import {DomSanitizer, SafeResourceUrl} from '@angular/platform-browser';
import {query} from "@angular/animations";

@Component({
  selector: 'app-manage-media',
  templateUrl: './manage-media.component.html',
  styleUrls: ['./manage-media.component.css']
})
export class ManageMediaComponent implements OnInit {
  mediaId: number;
  media: MediaDto;
  trailerLink: SafeResourceUrl;
  duration: string;

  constructor(
    private currentRoute: ActivatedRoute,
    private mediasClient: MediasClient,
    private sanitizer: DomSanitizer
  ) {
  }
  ngOnInit() {

    this.mediaId = Number(this.currentRoute.snapshot.paramMap.get('id'));
    this.mediasClient.get(this.mediaId).subscribe(
      result => {
        this.media = result;
        let hours = Math.floor(this.media.duration / 60);
        let minutes = this.media.duration % 60;
        this.duration = hours + 'h ' + minutes + 'm';
        this.trailerLink = this.sanitizer.bypassSecurityTrustResourceUrl(this.media.trailerLink);
      },
      error => console.error(error)
    );
  }
}
