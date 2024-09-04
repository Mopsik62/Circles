  mergeInto(LibraryManager.library, {


    RateGame: function () {
      ysdk.feedback.canReview()

          .then(({ value, reason }) => {
              if (value) {
                  ysdk.feedback.requestReview()
                      .then(({ feedbackSent }) => {
                          console.log(feedbackSent);
                          console.log("You rate the game");
                      })
              } else {
                  console.log(reason)
                  console.log("You can't rate the game");

              }
          })

    },

    SaveExtern: function (date) {
      var dateString = UTF8ToString(date);
      var myobj = JSON.parse(dateString);
      player.setData(myobj);
      //console.log("myobj");
      console.log(myobj);
      console.log("PlayerData was saved");

    },

    SetToLeaderboard: function(value) {
  ysdk.isAvailableMethod('leaderboards.setLeaderboardScore')
    .then(isAvailable => {
      if (isAvailable) {
        ysdk.getLeaderboards()
          .then(lb => {
            lb.setLeaderboardScore('HighScore', value);
          })
          .catch(err => {
            console.error('Ошибка получения leaderboards:', err);
          });
      } else {
        console.warn('Метод setLeaderboardScore недоступен для пользователя.');
      }
    })
    .catch(err => {
      console.error('Ошибка проверки доступности метода:', err);
    });
    },


    GameplayStart: function() {
        if (ysdk.features && ysdk.features.GameplayAPI) {
    ysdk.features.GameplayAPI.start();
    }
   },

   GameplayStop: function() {
        if (ysdk.features && ysdk.features.GameplayAPI) {
    ysdk.features.GameplayAPI.stop();
    }
   },
   

   GetLang: function() {
        // Получение языка из инициализированного SDK
        var lang = ysdk.environment.i18n.lang;
        console.log(lang);

        // Преобразование строки в буфер
        var bufferSize = lengthBytesUTF8(lang) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(lang, buffer, bufferSize);
        return buffer;
   },
   
    LoadExtern: async function() {
    try {
        // Ожидание инициализации игрока
        await initPlayer();

        if (player) {
            const _data = await player.getData();
            const myJSON = JSON.stringify(_data);
            //console.log("myJSON");
            console.log(myJSON);
            MyGameInstance.SendMessage('GameManager', 'LoadFromYandex', myJSON);
            console.log("PlayerData was loaded");
            if (ysdk.features && ysdk.features.LoadingAPI) {
    ysdk.features.LoadingAPI.ready();
}

        } else {
            console.error('Player not initialized');
            if (ysdk.features && ysdk.features.LoadingAPI) {
    ysdk.features.LoadingAPI.ready();
}

        }
    } catch (err) {
        console.error('Error loading player data:', err);
    }
    },

   ShowAdv : function(){
      ysdk.adv.showFullscreenAdv({
    callbacks: {
        onClose: function(wasShown) {
          this.GameplayStart();
          console.log("onClose");
          MyGameInstance.SendMessage('AudioManager', 'setUnmute');

          // Действие после закрытия рекламы.
        },
        onOpen: function(){
          this.GameplayStop();
          console.log("onOpen");

          MyGameInstance.SendMessage('AudioManager', 'setMute');

        },
        onError: function(error) {
          // Действие в случае ошибки.
          console.log("onError");

        }
    }
})
   },




    
  }); 