import {Component, OnInit} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import {MediaDto, MediasClient, SearchMediasQuery} from "../../web-api-client";

@Component({
  selector: 'app-manage-medias',
  templateUrl: './manage-medias.component.html',
  styleUrls: ['./manage-medias.component.css']
})
export class ManageMediasComponent implements OnInit {
  searchForm!: FormGroup;
  medias: MediaDto[];

  constructor(
    private formBuilder: FormBuilder,
    private mediasClient: MediasClient
  ) { }

  ngOnInit() {
    this.searchForm = this.formBuilder.group({
      title: ['']
    });

    let query = new SearchMediasQuery();
    query.title = this.searchForm.value.title;
    this.mediasClient.search(query).subscribe(
      result => {
        this.medias = result;
      },
      error => console.error(error)
    );
  }

  onSubmit() {
    if (this.searchForm.valid) {
      let query = new SearchMediasQuery();
      query.title = this.searchForm.value.title;
        this.mediasClient.search(query).subscribe(
          result => {
            this.medias = result;
          },
          error => console.error(error)
        );
    }
  }
}
