# Kemocade Unity Coverage Action
A GitHub Action for enforcing Unity Test Code Coverage.
This GitHub Action parses the output of the [Unity Code Coverage Package](https://docs.unity3d.com/Packages/com.unity.testtools.codecoverage@0.2/manual/CoverageTestRunner) and fails the action/github pull requests if Code Coverage requirements are not met.

Forked from [UnityCodeCoverage.Action](https://github.com/ActuatorDigital/UnityCodeCoverage.Action).
Updated to .NET 7 & C# 11.0 by [Dustuu](https://github.com/dustuu) from [Kemocade](https://github.com/kemocade).

## Inputs

### `coverage-file-path` - **Required** 
The path of the coverage file to parse.

Default: './artifacts/CodeCoverage/Report/Summary.xml'

### `required-coverage` - **Required**
The minimum percentage of code coverage requried for the action to sucessfully complete. 

Default: 75

## Example usage

> :warning: **As of August 2023, Unity has ended support for offline activation of Personal Licenses.**
> Due to this change, the `game-ci/unity-test-runner` step will now only work with Unity Plus or Unity Pro Licenses.
> You can still run Unit Tests manually from within the Unity Editor with a Personal License.

Create repository secrets with the names and values described below.
For details on how to create repository secrets, see [Creating Encrypted Secrets for a Repository](https://docs.github.com/en/actions/security-guides/encrypted-secrets#creating-encrypted-secrets-for-a-repository).
This time, make sure you are creating **repository secrets**, and not **repository variables**.

> :warning: **These secrets contain sensitive information that could compromise your Unity account if exposed!**
> To keep them safe, you should only ever input these values as GitHub Actions encrypted secrets!
> Never commit these secrets directly to source control!

* `UNITY_EMAIL`: The email address associated with your Unity account.
* `UNITY_PASSWORD`: The password for your Unity account.
* `UNITY_SERIAL`: Your Unity Plus or Unity Pro serial number.
  * For more information, see ["How do I find my license serial number?"](https://support.unity.com/hc/articles/209933966-How-do-I-find-my-license-serial-number).

```yml
jobs:
  test:
    runs-on: ubuntu-latest
    permissions:
      contents: write
      checks: write
    steps:
    # Run the Unit Tests and generate the Code Coverage report
    - name: Test Runner
      id: runner
      uses: game-ci/unity-test-runner@9d0bc623a78ee101f2c8956460d73bba2dfcf0c4
      env:
        UNITY_EMAIL: ${{ secrets.UNITY_EMAIL }}
        UNITY_PASSWORD: ${{ secrets.UNITY_PASSWORD }}
        UNITY_SERIAL: ${{ secrets.UNITY_SERIAL }}
      with:
        projectPath: path/to/your/project
        githubToken: ${{ secrets.GITHUB_TOKEN }}
        
    # Ensure that the Code Coverage report meets the user's configured requirements
    - name: Enforce Code Coverage
      uses: kemocade/Kemocade.Unity.Coverage.Action@ad06420f6fbb107bb3aa37f41fb99c79a3f93ad3
      with:
        coverage-file-path: ${{ steps.runner.outputs.coveragePath }}/Report/Summary.xml
        required-coverage: 100
```
