pipeline {
    agent any

    environment {
        PROJECT_PATH  = "SmartCarePatientPortal.csproj"
        BUILD_CONFIG  = "Release"
        TEST_PROJECT  = "SmartCarePatientPortal.TestOpenAIProject/SmartCarePatientPortal.TestOpenAIProject.csproj"
        PUBLISH_DIR   = "C:\\_Publish\\SmartCarePatientPortal_publish"
        IIS_SITE_PATH = "C:\\_Publish\\SmartCarePatientPortal"
        APP_POOL_NAME = "SmartCarePatientPortal"
    }

    stages {
        stage('Checkout') {
            steps {
                git branch: 'develop', url: 'https://github.com/Sreeram2710/SmartCarePatientPortal.git'
            }
        }

        stage('Run Unit Tests') {
            steps {
                bat "dotnet test %TEST_PROJECT% --configuration %BUILD_CONFIG% --no-build --logger trx"
            }
        }
        
        stage('Publish') {
            steps {
                bat "dotnet publish %PROJECT_PATH% -c %BUILD_CONFIG% -o %PUBLISH_DIR%"
            }
        }

        stage('Deploy to IIS') {
            steps {
                bat '''
                echo Stopping App Pool
                %windir%\\System32\\inetsrv\\appcmd stop apppool /apppool.name:"%APP_POOL_NAME%"

                echo Copying files to IIS folder
                robocopy "%PUBLISH_DIR%" "%IIS_SITE_PATH%" /MIR

                echo Starting App Pool
                %windir%\\System32\\inetsrv\\appcmd start apppool /apppool.name:"%APP_POOL_NAME%"
                '''
            }
        }
    }
}
