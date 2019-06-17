import { Component, ViewChild } from "@angular/core";
import { MatDialog, MatSort } from "@angular/material";
import { MatTableDataSource } from "@angular/material/table";
import { ActivatedRoute } from "@angular/router";
import { UserListing } from "../../models/user-listing.model";
import { User } from "../../models/user.model";
import { AdminService } from "../../services/admin.service";
import { AuthService } from "./../../services/auth.service";

@Component({
  selector: "app-user-list",
  templateUrl: "./user-list.component.html"
  
})
export class UserListComponent {
  @ViewChild(MatSort)
  sort: MatSort;
  userListing = new UserListing(0, []);
  isLoggedIn: boolean;
  displayedColumns: string[] = ["firstname", "lastname", "email"];
  pageSize = 10;
  dataSource = new MatTableDataSource<User>();

  constructor(
    private authService: AuthService,
    private adminService: AdminService,
    private dialog: MatDialog,
    private route: ActivatedRoute
  ) {
  }

  ngOnInit() {
    this.isLoggedIn = this.authService.isLoggedIn();
    this.getUsers();  
  }

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
  }
  
  getUsers(pageNumber = 0, postsPerPage = 10) {
    this.adminService.getUsers(pageNumber, postsPerPage)
      .subscribe(result => {
        this.userListing = result;
        this.dataSource.data = result.users;
      });
  }

  
}
