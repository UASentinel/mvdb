import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup} from "@angular/forms";
import {GenreDto, GenresClient, MediaDto, MediasClient, MediaType, SearchMediasQuery} from "../../web-api-client";

@Component({
  selector: 'app-manage-medias',
  templateUrl: './manage-medias.component.html',
  styleUrls: ['./manage-medias.component.css']
})
export class ManageMediasComponent implements OnInit {
  searchForm!: FormGroup;
  sortForm!: FormGroup;
  sortInitialValues: any;
  medias: MediaDto[];
  mediaTypes: MediaType[] = [MediaType.None, MediaType.Movie, MediaType.Series];
  genres: GenreDto[];
  pageSize: number = 5;
  pageCount: number;
  currentPage: number;
  selectedMedias: MediaDto[];

  constructor(
    private formBuilder: FormBuilder,
    private mediasClient: MediasClient,
    private genresClient: GenresClient
  ) { }

  ngOnInit() {
    this.genresClient.getAll().subscribe(
      result => {
        this.genres = result;
      },
      error => console.error(error)
    );

    this.searchForm = this.formBuilder.group({
      title: [''],
      mediaType: [MediaType[this.mediaTypes[0]]]
    });

    this.sortForm = this.formBuilder.group({
      orderBy: [''],
      order: ['asc']
    });

    this.sortInitialValues = this.sortForm.value;

    let query = new SearchMediasQuery();
    query.title = this.searchForm.value.title;
    this.mediasClient.search(query).subscribe(
      result => {
        this.medias = result;
        this.pageCount = Math.ceil(this.medias.length / this.pageSize);
        this.selectPage(1);
      },
      error => console.error(error)
    );
  }

  onSearchSubmit() {
    if (this.searchForm.valid) {
      const query = {
        title: this.searchForm.value.title,
        mediaType: MediaType[this.searchForm.value.mediaType as keyof typeof MediaType]
      } as SearchMediasQuery;

      this.mediasClient.search(query).subscribe(
        result => {
          this.medias = result;
          this.pageCount = Math.ceil(this.medias.length / this.pageSize);
          this.selectPage(1);
          this.sortForm.reset(this.sortInitialValues);
        },
        error => console.error(error)
      );
    }
  }

  previousPage(): void {
    const targetPage = this.currentPage - 1;
    if(targetPage < 1 || targetPage > this.pageCount){
      return;
    }

    this.selectPage(targetPage);
  }

  nextPage(): void {
    const targetPage = this.currentPage + 1;
    if(targetPage < 1 || targetPage > this.pageCount){
      return;
    }

    this.selectPage(targetPage);
  }

  selectPage(targetPage: number): void {
    if(targetPage < 1 || targetPage > this.pageCount){
      return;
    }

    const firstIndex = (targetPage - 1) * this.pageSize;
    const lastIndex = Math.min(this.medias.length, (targetPage) * this.pageSize);

    this.selectedMedias = this.medias.slice(firstIndex, lastIndex)

    this.currentPage = targetPage;
  }

  onOrderByChange(orderByElement: HTMLInputElement): void {
    const orderBy = orderByElement.value.toString();
    this.sortMedias(orderBy, this.sortForm.value.order);

    this.selectPage(this.currentPage);
  }

  onOrderChange(orderElement: HTMLInputElement): void {
    const order = orderElement.value.toString();
    this.sortMedias(this.sortForm.value.orderBy, order);

    this.selectPage(this.currentPage);
  }

  sortMedias(orderBy: string, order: string): void {
    if(orderBy === ''){
      this.medias.reverse();
      return;
    }

    if(order === 'desc'){
      this.medias.sort((a, b) => (a[orderBy] > b[orderBy]) ? -1 : 1);
    }
    else{
      this.medias.sort((a, b) => (a[orderBy] > b[orderBy]) ? 1 : -1);
    }
  }

  protected readonly MediaType = MediaType;
}
