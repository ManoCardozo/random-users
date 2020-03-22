import { IUserModel } from '../interfaces/IUserModel'

export class UserModel implements IUserModel {
  userId: string;
  title: number;
  firstName: number;
  lastname: string;
  dateOfBirth: Date;
  phoneNumber: string;
  fullName: string;
  profileImageData: string;
  profileThumbnailData: string;
  dateOfBirthPretty: string;
}
