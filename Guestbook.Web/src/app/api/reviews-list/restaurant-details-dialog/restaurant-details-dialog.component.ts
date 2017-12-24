
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { MdDialogRef } from '@angular/material';
import { MessageService } from '../../../shared/services/message.service';
import { Review } from '../../contracts/review';
import { RestaurantDetailsDialogService } from "./restaurant-details-dialog.service";
import { Restaurant } from "../../contracts/restaurant";

const EMAIL_REGEX = /^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;

@Component({
  selector: 'app-restaurant-details-dialog',
  templateUrl: './restaurant-details-dialog.component.html',
  styleUrls: ['./restaurant-details-dialog.component.css'],
  providers: [RestaurantDetailsDialogService]
})
export class RestaurantDetailsDialogComponent implements OnInit {
  restaurant: Restaurant = new Restaurant();

  constructor(
    private dialogRef: MdDialogRef<RestaurantDetailsDialogComponent>,
    private router: Router,
    private route: ActivatedRoute,
    private messageService: MessageService,
    private service: RestaurantDetailsDialogService,
  ) {
  }


  ngOnInit() {
  }

  onCreateRestaurant(form: FormGroup) {
    if (form.valid) {
         this.service.createRestaurant(this.restaurant)
        .subscribe(m => {
          this.messageService.success('Ресторан " + this.restaurant.name + " успешно добавлен.');
          this.onCancel();
        }, (error) => {
          this.messageService.error(error.json());
          this.onCancel();
        });
    }
  }


  onCancel() {
    this.dialogRef.close();
  }
}
