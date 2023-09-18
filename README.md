# BlueteethXamarinPrototype
Blueteeth is a dental healthcare project, which aims to analyze how well people brush
their teeth, give them tips, and improve their oral habits based on the movements and time spent brushing in each mouth quadrant.

The project consists of two parts – an attachable device with sensors in it and a mobile 
app, which connects via Bluetooth to the device, extracts the data, and converts it into 
useable, user-friendly graphics.

## 1. Used Technologies
### Programming Language:
- C#
- Arduino (based on C)
### Markup Language:
- XML
### Database:
- Firebase Firestore Database
### Authentication Service:
- Auth0
### Mobile App Development platforms:
- Firebase
- Xamarin
### Hardware Test Programs:
- Circuit.io
### IDEs:
- Visual Studio
- Arduino IDE
### Hardware Design Software:
- Blender
### Device Hardware:
- BLE Nano (based on Arduino Nano 33 BLE)
- Realtime Clock
- Accelerometer
- Gyroscope
- Flash Memory (For data storage when the brushing is done without the module being connected to a phone)
- Li-po Battery
- Li-po Battery Charging Module
- Speaker
- Light-emitting Diode
- 3D Printed Case
## 2. Work Process, Problems, and Solutions

### Phase 1: Role Assignment and Work Process Planning 
This was my first project, which was based entirely on my idea and whose only leader was me. I 
also had to pick my own team. I invited three of my friends, who are incredibly good at 
Programming, 3D modelling, Physics and Mathematics, to join my team and when I pitched my 
idea, they at once accepted my offer to become part of the project.

I was not only leading the project, but I was also developing the mobile app alongside one of my 
teammates, helping our 3D designer with the models of the device, preparing the documentation 
of the project, working on the marketing side, and last but not least, presenting the project at 
competition, startup weekends, conferences, mentors, etc.

Similarly to the last two projects, I decided that it is better to work on the modules of the project –
the device and the mobile app – simultaneously.
At the same time, one of the boys and I had to work on the business side of the project as we
transformed Blueteeth into a training company and once again joined Junior Achievement’s 
Student’s Company program. 
 
### Phase 2: Parts Research 
One of the most challenging phases of the project was to find the right sensors and 
microcontroller in terms of accuracy, performance, power-demand, and size. One of my 
teammates and I searched for two-three weeks which would be the best sensors for our module. 
We compared their compatibility, calculated the power- and performance-demand of the whole 
circuit and choose what size and type of batteries should we use. Then we were ready to purchase 
the sensors (from China, which proved to be a big mistake and we had to wait for them three 
months.

### Phase 3: Mobile App Setup and Device Design 
While we were waiting the sensors to arrive, we decided to focus on the mobile application and 
the design of the device.
For the mobile app we decided to use Xamarin as platform as it gave us the flexibility to work with 
both Android and IOS smart devices by creating a single cross-platform program (MAUI was not 
available at the time).

For the design of the device, we used Blender. At first, we were considering a design where the 
device attaches to the bottom of the toothbrush. After we printed it, it turned out that it was a 
terrible idea. That is when we decided that the physical module should attach to the toothbrush 
sideways. We made several variations of that designs (most of which you can find in Figure 3.2.2)

### Phase 4: Database and Authentication Setup 
At the beginning, we choose to work with SQLite as a database, because it does not require a lot 
of storage space and has all the features we needed. However, we ended up using Google’s 
Firebase Firestore Database, because it allowed us to smoothly update and merge online and 
offline data. We also decided to use the Firebase Authentication System. Unfortunately, there was 
a problem with the compatibility of the Firebase Auth System and the Firestore Database, which 
forced us to find another authentication service. After some research, the software department of 
the team and I decided to use the Auth0 service as it was secure enough and easy to integrate 
into our app.

### Phase 5: Sensor Testing 
When the sensors arrived, we had to connect them together and test the whole system. 
Fortunately, we could write the full code earlier as there are platforms where a person can select 
the sensors they want to test, upload their code, and run the program on the virtual 
microcontroller.

### Phase 6: Last Design Change 
When we 3D printed the case, we found out that we had not positioned the sensors properly, when 
we were designing the case of the device. The reason for that was the fact that we did not take 
into consideration the size of the cables (which are enormously spacey, especially when you do not
have space! Who could have guessed!? XD)
That is why we had to redesign the whole case of the physical module. Fortunately, we came up 
with an idea for the new case quickly (and this time we did not forget to include the cables in the 
3D model!).

### Phase 7: Data Transfer and Mobile App Visualization 
After we successfully tested the sensors and changed the design of the physical device, we had to 
finish the Mobile application, which meant that we had to set up the data transfer from the 
physical module to the mobile app and the data storage in the Firestore Database.
We also had to make sure that a record of a toothbrush is not lost in case the device is not 
connected to a phone. That is why we added a flash memory sensor, which keeps the
toothbrushing records for 30 days, and when a phone connects to the physical module, the 
unsend data is transferred to the mobile device and then uploaded/inserted into the database.

We also had minor problems with the visualization of the data in the application – the teeth, which 
show which zone is brushed properly, were not properly aligned for every type of screen. We later 
found out that we had mixed px and dp as size units, which led to a complete mess. Fortunately, 
it was not hard at all to fix that problem (we just switched all occurrences of px into dp and voila –
it worked :D).

## 3. Final Results
We managed to create the Blueteeth prototype with a working mobile app and a physical module, 
which records data and sends the information to the phone
