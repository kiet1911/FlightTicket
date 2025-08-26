<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet"> 
<link href="https://getbootstrap.com/docs/5.2/assets/css/docs.css" rel="stylesheet"> 
<h1 style="font-weight:bold">Project website Airline ticket managment</h1>
</hr> 
<h3>Team size : 1 </h3> 
<div style="display: flex ; flex-direction: row ; justify-content: center ; align-content: center">
<table>
  <thead>
    <tr>
      <th>Name</th>
      <th>ID Student</th>
      <th>Role</th>
    </tr>
  </thead>
  <tbody>
     <tr>
      <td>Nguyến Tuấn Kiệt</td>
      <td>20DH110204</td>
      <td>FullStack</td>
    </tr>
  </tbody>
</table>
</div>
<h1 style="font-weight:bold">Project Introduce</h1>
</hr>
<div><span style="font-weight:bold">Desciption: </span><span>Developed a web-based airline ticket booking system featuring real-time seat selection, user account management, transaction history tracking, and flight schedule search and more features</span></div>
<h1 style="font-weight:bold">Technologies Used :</h1>
</hr>
<ul>
  <li>Backend : .Net MVC (Framework 4.7.8)</li>
  <li>FrontEnd : Razor , HTML , CSS , JavaScript , jQuery/Ajax , Bootstrap</li>
  <li>Database : Sql Server</li>
  <li>IDE : Visual Studio (2019) </li>
</ul>
<h1 style="font-weight:bold">Some Main Features on the website :</h1>
</hr>
<ul>
  <li>Admin Control: Manage flights, bookings, and others </li>
  <li>User: Order tickets, pay tickets, login/register and others</li>
  <li>Authentication: using Session to store User data for accessing permission </li>
  <li>Seat booking: Real-time booking with Ajax/jQuery, Schedule updates  with Hangfire(C# Nuget)</li>
  <li>Transaction History: View, paginate, and manage transactions </li>
  <li>Searching Flight schedule: Indexing(SQL server) to optimize search speed</li>
</ul>
<h1 style="font-weight:bold">Manager Task :</h1>
</hr>
<a href="https://docs.google.com/spreadsheets/d/1q36mYsDpg0KSqa1xVAboEPSbTSSZeBuuDQRc_nyQF2w/edit?usp=sharing">Link to Excel File</a>
<h1 style="font-weight:bold">Setup SQL Server :</h1>
</hr>
<a href="https://drive.google.com/file/d/1zdXCdiVZdRSOM_yHML6RL4xDAPXVpGsj/view?usp=drive_link">Link to SQL File</a>
</hr> 
<h1>Account for Testing :</h1>
<p>Account in admin site : userName: droang01@gmail.com, password: 123456</p>
<p>Account in WEB API site : userName: droang01@gmail.com, password: 123456 for admin</p> 
<p>Account in WEB API site : userName: asdsaphat@gmail.com, password: 123456T@ for employee</p>
<p>Account in Customer site : userName: droang09@gmail.com, password: Kuphe1980</p>
<p>Account in Customer paypal account : userName: sb-opsgg43593834@personal.example.com, password: Kj'PG+9;</p> <p>Login account paypal in this site  https://www.sandbox.paypal.com/signin</p> 
</hr>
<h1 style="font-weight:bold">Setup MVC Project :</h1>
</hr>
<span>Please change the APIs key Paypal by your API in file WebConfig if you use your own sandbox paypal</span>
</br>
<img width="392" height="160" alt="image" src="https://github.com/user-attachments/assets/a8e77e5a-b73a-4ad4-ba86-c979569989ea" />
</br>
<span>Please change the Sql Server connection if you use your own database</span>
</br>
<img width="698" height="60" alt="image" src="https://github.com/user-attachments/assets/6b309049-61aa-49d3-8de3-7f3c3202aa69" />

<h1 style="font-weight:bold">Some Interface features :</h1>
</hr>
<h6>1. HomePage with searching feature</h6>
<img width="1824" height="886" alt="image" src="https://github.com/user-attachments/assets/2373f0a8-e942-4ae0-a74c-0669c65d74fa" />
<h6>2. Login and Register</h6>
<img width="1818" height="877" alt="image" src="https://github.com/user-attachments/assets/6a3b9347-d32a-4228-a0d3-9736155bda0b" />
<img width="1823" height="880" alt="image" src="https://github.com/user-attachments/assets/13986c27-5a73-410e-b8b0-6e6161d0c25f" />
<h6>3. User profile and User Ticket</h6>
<img width="1811" height="887" alt="image" src="https://github.com/user-attachments/assets/b54f59e4-d833-4581-b8eb-a24cf7a2ead0" />
<img width="1816" height="887" alt="image" src="https://github.com/user-attachments/assets/d1eb3ac0-0c9f-4f7e-9154-6b54f8679dd5" />
<img width="1819" height="879" alt="image" src="https://github.com/user-attachments/assets/de274d0f-a2b2-4b3a-a464-29f4e590d9be" />
<p>=> you can also print the ticket to PDF file</p>
<img width="1840" height="883" alt="image" src="https://github.com/user-attachments/assets/a2a3ab48-8236-4b65-a7a0-0c5cddb8fae3" />
<h6>4. Make a Order Ticket with real time seating</h6>
<img width="1819" height="879" alt="image" src="https://github.com/user-attachments/assets/a48a2b65-f4a9-438a-ba9b-713410123a94" />
<p>=> only find the flight schedules have datetime greater than or equal to today</p>
<img width="1817" height="884" alt="image" src="https://github.com/user-attachments/assets/cf80b8b9-5455-4480-986d-3e8ba3bb7523" />
<p>=> you can buy many ticket depend on the seat you choose , you can set your baggages</p>
<img width="1818" height="870" alt="image" src="https://github.com/user-attachments/assets/81ed3e9f-4e43-43e4-8d41-1981ad5bfbd7" />
<img width="1845" height="878" alt="image" src="https://github.com/user-attachments/assets/5ca1aab1-19a7-48ad-a976-e8b458323908" />
<img width="1845" height="883" alt="image" src="https://github.com/user-attachments/assets/63450f1d-1bd1-497b-b16e-9cf66e0f7179" />
<img width="1816" height="881" alt="image" src="https://github.com/user-attachments/assets/61866af2-554c-4adf-b5f8-e0c023524580" />
<h6>5. The manager system by Admin and Employee </h6>
<img width="1811" height="880" alt="image" src="https://github.com/user-attachments/assets/2587e940-c7fe-43ea-b3ab-446f9e708e81" />
<p>=> it has many feature , you can discover by your self . The relationship between data is very strong , so that the table which 
has primary key be used by another table will not be deleted . Unless you change the Cascade to Delete</p>


